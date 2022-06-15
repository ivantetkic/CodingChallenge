namespace HospitalSimulatorConsole.Infrastructure
{
    /// <summary>
    ///     The service that apply drugs to the patient simulate future patients’ state.
    /// </summary>
    public interface IHospitalService
    {
        Task ExecuteAsync(string[] args, CancellationToken stoppingToken = default);
    }
}