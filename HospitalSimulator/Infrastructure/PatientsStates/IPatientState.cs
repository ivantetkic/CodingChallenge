using HospitalSimulatorConsole.Infrastructure.Drugs;
using HospitalSimulatorConsole.Infrastructure.Rules;

namespace HospitalSimulatorConsole.Infrastructure.PatientsStates
{
    /// <summary>
    ///     It provides the state of the patient that can be created.
    ///     It contains list of drugs that is added once the effect is added to the drug
    ///     It contains the Rule object that is initialazed in constructor
    ///     Available is AddDrugState method that is used to drugs to the drug list.
    /// </summary>
    public interface IPatientState
    {
        string Code { get; }
        IRule Rule { get; set; }
        List<IDrugState> Drugs { get; set; }

        void AddDrugState(IDrugState drugState);
    }
}