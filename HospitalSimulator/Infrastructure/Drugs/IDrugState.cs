using HospitalSimulatorConsole.Infrastructure.PatientsStates;

namespace HospitalSimulatorConsole.Infrastructure.Drugs
{
    /// <summary>
    /// It provides the state of the drug that can be created.
    /// Available is AddEffect overloaded method that is used to add effect to patient if the drug is applied.
    /// On calling AddEffect, the drug is automatically added to the global patient state list
    /// </summary>
    public interface IDrugState
    {
        string Code { get; }
        List<DrugEffect> Effects { get; }

        /// <summary>
        ///  Used to add effect to patient if the drug is applied.
        ///  The drug is automatically added to the global patient state list.
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        void AddEffect(IPatientState state, IPatientState becomeState);

        /// <summary>
        /// Used to add effect to patient if the drug is mixed with another drug.
        /// The drug is automatically added to the global patient state list.
        /// Effect is also added to the mixed drug
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        /// <param name="mixDrug">Another drug applied with the current one</param>
        void AddEffect(IPatientState state, IPatientState becomeState, IDrugState mixDrug);

        /// <summary>
        /// Used to add effect to patient if the drug is required.
        /// The drug is automatically added to the global patient state list.
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        /// <param name="isRequired">If "true" drug has to be applied to State, if not applied State will become BecomeState</param>
        void AddEffect(IPatientState state, IPatientState becomeState, bool isRequired);

        /// <summary>
        /// Used to add effect to patient if the drug is mixed with another drug and is required.
        /// The drug is automatically added to the global patient state list.
        /// Effect is also added to the mixed drug
        /// </summary>
        /// <param name="state">Begin patient state</param>
        /// <param name="becomeState">Become patient state</param>
        /// /// <param name="mixDrug"></param>
        /// <param name="isRequired">If "true" drug has to be applied to State, if not applied State will become BecomeState</param>
        void AddEffect(IPatientState state, IPatientState becomeState, IDrugState mixDrug, bool isRequired);
        
    }
}