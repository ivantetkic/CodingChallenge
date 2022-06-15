using HospitalSimulatorConsole.Infrastructure.PatientsStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSimulatorConsole.Infrastructure.Drugs
{
    /// <summary>
    ///     The class provides the state of the drug that can be created.
    ///     In constructor you have to provide "Code" of the drug.
    ///     It contains list of effects that is initialized on start
    ///     Available is AddEffect overloaded method that is used to add effect to patient if the drug is applied.
    ///     On calling AddEffect, the drug is automatically added to the global patient state list
    /// </summary>
    public class DrugState : IDrugState
    {

        public DrugState(string code)
        {
            this.Code = code;

            effects = new List<DrugEffect>();

            // if Drug is not in HospitalService list add them on initialization
            // all initialled drugs has to be on service list
            if (!HospitalService.PatientDrugs.Any(x => x.Code == code))
                HospitalService.PatientDrugs.Add(this);
        }

        private List<DrugEffect> effects;

        public string Code { get; private set; }
        public List<DrugEffect> Effects { get { return effects; } private set { } }


        /// <summary>
        ///  Used to add effect to patient if the drug is applied.
        ///  The drug is automatically added to the global patient state list.
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        public void AddEffect(IPatientState state, IPatientState becomeState )
        {
            effects.Add(new DrugEffect(this, state, becomeState));
            state?.AddDrugState(this);
        }

        /// <summary>
        /// Used to add effect to patient if the drug is mixed with another drug.
        /// The drug is automatically added to the global patient state list.
        /// Effect is also added to the mixed drug
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        /// <param name="mixDrug">Another drug applied with the current one</param>
        public void AddEffect(IPatientState state, IPatientState becomeState, IDrugState mixDrug)
        {
            effects.Add(new DrugEffect(this, state, becomeState, mixDrug));
            addEffectToMixDrug(state, becomeState, mixDrug, false);
            state?.AddDrugState(this);
        }

        /// <summary>
        /// Used to add effect to patient if the drug is required.
        /// The drug is automatically added to the global patient state list.
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        /// <param name="isRequired">If "true" drug has to be applied to State, if not applied State will become BecomeState</param>
        public void AddEffect(IPatientState state, IPatientState becomeState, bool isRequired)
        {
            effects.Add(new DrugEffect(this, state, becomeState, isRequired));
            state?.AddDrugState(this);
        }

        /// <summary>
        /// Used to add effect to patient if the drug is mixed with another drug and is required.
        /// The drug is automatically added to the global patient state list.
        /// Effect is also added to the mixed drug
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        /// /// <param name="mixDrug"></param>
        /// <param name="isRequired">If "true" drug has to be applied to State, if not applied State will become BecomeState</param>

        public void AddEffect(IPatientState state, IPatientState becomeState, IDrugState mixDrug, bool isRequired)
        {
            effects.Add(new DrugEffect(this, state, becomeState, mixDrug, isRequired));
            addEffectToMixDrug(state, becomeState, mixDrug, isRequired);
            state?.AddDrugState(this);
        }

        /// <summary>
        /// Used to add the same effect to the mixed drug as it is on current drug.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="becomeState"></param>
        /// <param name="mixDrug"></param>
        /// <param name="isRequired"></param>
        private void addEffectToMixDrug(IPatientState state, IPatientState becomeState, IDrugState mixDrug, bool isRequired)
        {
            if (mixDrug != null)
            {
                if (mixDrug.Effects.Any(x => (x.MixedDrug ?? mixDrug).Code == mixDrug.Code) == false)
                {
                    mixDrug.AddEffect(state, becomeState, this, isRequired);
                }

            }
        }
    }
    
}
