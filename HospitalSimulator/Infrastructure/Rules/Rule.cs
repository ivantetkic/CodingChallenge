using HospitalSimulatorConsole.Infrastructure.Constants;
using HospitalSimulatorConsole.Infrastructure.Drugs;
using HospitalSimulatorConsole.Infrastructure.PatientsStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSimulatorConsole.Infrastructure.Rules
{
    /// <summary>
    ///     It contains the rules that is applied to the patient with the consumed drugs. 
    ///     Contains Apply method that run all rules that is prepared. 
    ///     Initial patient state is changed based on thease rules.
    ///     Applied rule changes the state of patient and thats why the rules order is important.
    /// </summary>
    public class Rule :IRule
    {

        List<Func<List<IDrugState>, IPatientState, IPatientState>> _rules;
        IPatientState _state;

        public Rule(string patientCode)
        {

            _rules = new List<Func<List<IDrugState>, IPatientState, IPatientState>>();

            //populates list of rules with prepared rules, the order of rules are important.
            _rules.Add(requiredDrugs);          
            _rules.Add(mixDrugs);
            _rules.Add(notMixedDrugs);
            _rules.Add(noodlyPower);

            _state = HospitalService.PatientStates.First(x => x.Code == patientCode);

        }

        /// <summary>
        ///     Runs all the rules that is added to the list of rules.
        /// </summary>
        /// <param name="drugs">List of drugs that will be applied to the patient</param>
        /// <returns></returns>
        public IPatientState Apply(List<IDrugState> drugs)
        {
            IPatientState becomeState = _state;

            foreach (var rule in _rules)
            {
                becomeState = rule.Invoke(drugs, becomeState);
            }

            return becomeState;
        }

        /// <summary>
        ///     Rule that is applied if the drug has some effect with another drug
        /// </summary>
        /// <param name="drugs">Drugs that will be applied</param>
        /// <param name="becomeState">State that can be changed is the drug is applied</param>
        /// <returns></returns>
        private IPatientState mixDrugs(List<IDrugState> drugs, IPatientState becomeState)
        {
            /// all effects of the drug mixed with another drug
            var effects = drugs.SelectMany(x => x.Effects).Where(x => x.MixedDrug != null && !x.IsRequired && (x.State == null ? becomeState.Code : x.State.Code) == becomeState.Code);

            /// if there are any effects that coause dead patient will be dead otherwise apply effect
            if (becomeState.Code == Patient.Dead || effects.Any(x => x.BecomeState.Code == Patient.Dead && drugs.Any(y => y.Code == x.MixedDrug?.Code)))
            {
                becomeState = HospitalService.PatientStates.First(x => x.Code == Patient.Dead);
            }
            else
            {
                var sideEffect = effects.Where(x => drugs.Any(y => y.Code == x.MixedDrug?.Code));
                becomeState = sideEffect.FirstOrDefault()?.BecomeState ?? becomeState;
            }

            return becomeState;
        }

        /// <summary>
        ///     Rule that is applied if the drug does not have mixed drug effect
        /// </summary>
        /// <param name="drugs">Drugs that will be applied</param>
        /// <param name="becomeState">State that can be changed is the drug is applied</param>
        /// <returns></returns>
        private IPatientState notMixedDrugs(List<IDrugState> drugs, IPatientState becomeState)
        {
            /// all effects of the drug excluded effects of the drugs that is mixed with another drug
            var effects = drugs.SelectMany(x => x.Effects).Where(x => x.MixedDrug == null && !x.IsRequired && (x.State == null ? becomeState.Code : x.State.Code) == becomeState.Code);

            /// if there are any effects that coause dead patient will be dead otherwise apply effect
            if (becomeState.Code == Patient.Dead || effects.Any(x => x.BecomeState.Code == Patient.Dead))
            {
                becomeState = HospitalService.PatientStates.First(x => x.Code == Patient.Dead);
            }
            else
            {
                becomeState = effects.FirstOrDefault()?.BecomeState ?? becomeState;
            }

            return becomeState;
        }

        /// <summary>
        ///     Rule that is applied if some drug is required but not provided
        /// </summary>
        /// <param name="drugs">Drugs that will be applied</param>
        /// <param name="becomeState">State that can be changed is the drug is applied</param>
        /// <returns></returns>
        private IPatientState requiredDrugs(List<IDrugState> drugs, IPatientState becomeState)
        {
            /// list of required drugs that should be applied to the patient
            var requiredDrugCodes = _state.Drugs.SelectMany(x => x.Effects)
                                    .Where(x => x.IsRequired && x.State.Code == _state.Code)
                                    .SelectMany(x => x.State.Drugs).
                                    Select(x => x.Code).Distinct();

            /// if all required drugs are not applied than the effect will be applied
            if (becomeState.Code != Patient.Dead && !requiredDrugCodes.All(x => drugs.Any(y => y.Code == x)))
            {
                var requiredBecome = _state.Drugs.SelectMany(x => x.Effects).Where(x => x.IsRequired).FirstOrDefault();
                becomeState = requiredBecome?.BecomeState ?? becomeState;
            }

            return becomeState;
        }

        /// <summary>
        ///     One time in a million the Flying Flying Spaghetti Monster shows his noodly power and 
        ///     resurrects a dead patient(Dead becomes Healthy).
        /// </summary>
        /// <param name="drugs">Drugs that will be applied</param>
        /// <param name="becomeState">State that can be changed is the drug is applied</param>
        /// <returns></returns>
        private IPatientState noodlyPower(List<IDrugState> drugs, IPatientState becomeState)
        {
            var rnd = new Random();
            rnd.Next(0, 1000000);

            if (rnd.Next(0, 1000000) == 666)
            {
                if (becomeState.Code == Patient.Dead)
                {
                    becomeState = HospitalService.PatientStates.First(x => x.Code == Patient.Healthy);
                }
            }

            return becomeState;
        }
    }
}
