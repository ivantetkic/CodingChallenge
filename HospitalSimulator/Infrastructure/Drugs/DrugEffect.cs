using HospitalSimulatorConsole.Infrastructure.PatientsStates;

namespace HospitalSimulatorConsole.Infrastructure.Drugs
{
    /// <summary>
    /// The class describe effects of the drug. 
    /// Mandatory properties are:
    ///     DrugState - Drug that has this effect
    ///     State - initial patient's state  
    ///     BecomeState - state after the drug is applied
    /// Additional properties are:
    ///     MixedDrug - another drug applied with the current one
    ///     IsRequired - default "false", if "true" drug has to be applied to State, if not applied State will become BecomeState
    /// </summary>
    public class DrugEffect
    {
        public DrugEffect(IDrugState drugState, IPatientState state, IPatientState becomeState)
        {
            this.DrugState = drugState;
            this.State = state;
            this.BecomeState = becomeState;        
        }
        public DrugEffect(IDrugState drugState, IPatientState state, IPatientState becomeState, IDrugState mixedDrug)
        {
            this.DrugState = drugState;
            this.State = state;
            this.BecomeState = becomeState;
            this.MixedDrug = mixedDrug;
        }
        public DrugEffect(IDrugState drugState, IPatientState state, IPatientState becomeState, bool isRequired)
        {
            this.DrugState = drugState;
            this.State = state;
            this.BecomeState = becomeState;
            this.IsRequired = isRequired;
        }
        public DrugEffect(IDrugState drugState, IPatientState state, IPatientState becomeState, IDrugState mixedDrug, bool isRequired)
        {
            this.DrugState = drugState;
            this.State = state;
            this.BecomeState = becomeState;
            this.MixedDrug = mixedDrug;
            this.IsRequired = isRequired;
        }
        public IPatientState State { get; set; }
        public IPatientState BecomeState { get; set; }
        public IDrugState? MixedDrug { get; set; }
        public bool IsRequired { get; set; }
        public IDrugState DrugState { get; set; }
    }
    
}
