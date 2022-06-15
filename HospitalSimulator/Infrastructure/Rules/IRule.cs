using HospitalSimulatorConsole.Infrastructure.Drugs;
using HospitalSimulatorConsole.Infrastructure.PatientsStates;

namespace HospitalSimulatorConsole.Infrastructure.Rules
{
    /// <summary>
    ///     Contains Apply method that run all rules that is prepared. 
    /// </summary>
    public interface IRule
    {
        /// <summary>
        ///     Runs all the rules that is prepared.
        /// </summary>
        /// <param name="drugs">List of drugs that will be applied to the patient</param>
        /// <returns></returns>
        public IPatientState Apply(List<IDrugState> drugs);
       
    }
}