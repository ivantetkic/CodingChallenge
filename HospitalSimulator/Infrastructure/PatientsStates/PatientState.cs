using HospitalSimulatorConsole.Infrastructure.Drugs;
using HospitalSimulatorConsole.Infrastructure.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSimulatorConsole.Infrastructure.PatientsStates
{
    /// <summary>
    ///     The class provides the state of the patient that can be created.
    ///     In constructor you have to provide "Code" of the patient.
    ///     It contains list of drugs that is added once the effect is added to the drug
    ///     It contains the Rule object that is initialazed in constructor
    ///     Available is AddDrugState method that is used to drugs to the drug list.
    /// </summary>
    public class PatientState : IPatientState
    {
        public PatientState(string code)
        {
            this.Code = code;

            Drugs = new List<IDrugState>();

            // if PatientState is not in HospitalService list add them on initialization
            // all initialled patient states has to be on service list
            if (!HospitalService.PatientStates.Any(x => x.Code == code))
                   HospitalService.PatientStates.Add(this);

            Rule = new Rule(Code);
        }

        public string Code { get; private set; }

        public IRule? Rule { get; set; }
        public List<IDrugState> Drugs { get; set; }

        /// <summary>
        ///     Add Drug state to the Drugs list
        /// </summary>
        /// <param name="drugState">Drug that will be added to the Drugs list</param>
        public void AddDrugState(IDrugState drugState)
        {
            Drugs.Add(drugState);
        }
    }
}
