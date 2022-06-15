using HospitalSimulatorConsole.Infrastructure.Drugs;
using HospitalSimulatorConsole.Infrastructure.PatientsStates;
using HospitalSimulatorConsole.Infrastructure.Constants;
using Microsoft.Extensions.Logging;

namespace HospitalSimulatorConsole.Infrastructure
{

    /// <summary>
    ///     The service that apply drugs to the patient simulate future patients’ state.
    /// </summary>
    public class HospitalService : IHospitalService
    {
        private readonly ILogger<HospitalService> _logger;
        public static List<IPatientState> PatientStates = new List<IPatientState>();
        public static List<IDrugState> PatientDrugs = new List<IDrugState>();


        public HospitalService(ILogger<HospitalService> logger)
        {
            _logger = logger;
            initialize();
        }
        /// <summary>
        ///     Main method that runs the simulation.
        /// </summary>
        /// <param name="args">Input parameters from the user</param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(string[] args, CancellationToken stoppingToken = default)
        {

            /// checks if the params are provided

            string _patients = "";

            if (args.Length > 0)
            {
                _patients = args[0];
            }

            string _drugs = "";
            if (args.Length > 1)
            {
                _drugs = args[1];
            }

            /// Runs the simulation
            var results = await Task.Run(() => RunCheck(_patients, _drugs));
            Console.WriteLine(results);

        }



        /// <summary>
        ///     The method that runs the simulation
        /// </summary>
        /// <param name="_patients">Patients for simulation</param>
        /// <param name="_drugs">Drugs that will be applied to the patients</param>
        /// <returns>
        ///     Returns future patients state
        /// </returns>
        public static string RunCheck(string _patients, string _drugs)
        {

            initialize();
           
            var patients = _patients.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>();
            var drugs = _drugs.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>();

            List<IPatientState> results = new List<IPatientState>();


            foreach (var patient in patients)
            {
                var pState = PatientStates.FirstOrDefault(x => x.Code == patient) ?? new PatientState("");

                var pDrugs = PatientDrugs.Where(x => drugs.Any(y => y == x.Code)).ToList();

                if (pState.Rule != null)
                    results.Add(pState.Rule.Apply(pDrugs));
            }

            var returns = from all in Patient.GetAll
                         join res in results.GroupBy(x => x.Code)
                         on all equals res.Key
                         into mixes

                         from mix in mixes.DefaultIfEmpty()
                         select all + ":" + (mix?.Count() ?? 0);

            return string.Join(",", returns);
        }

        /// <summary>
        ///     Initialize the patients, drugs and the effects of the drugs
        /// </summary>
        private static void initialize()
        {
            if (!PatientStates.Any() && !PatientDrugs.Any()) { 

                IPatientState fever = new PatientState(Patient.Fever);
                IPatientState healthy = new PatientState(Patient.Healthy);
                IPatientState diabetes = new PatientState(Patient.Diabetes);
                IPatientState tuberculosis = new PatientState(Patient.Tuberculosis);
                IPatientState dead = new PatientState(Patient.Dead);
            
                IDrugState aspirin = new DrugState(Drug.Aspirin);
                aspirin.AddEffect(fever, healthy);


                IDrugState antibiotic = new DrugState(Drug.Antibiotic);
                antibiotic.AddEffect(tuberculosis, healthy);


                IDrugState insulin = new DrugState(Drug.Insulin);
                insulin.AddEffect(diabetes, diabetes);
                insulin.AddEffect(diabetes, dead, true);
                insulin.AddEffect(healthy, fever, antibiotic);


                IDrugState paracetamol = new DrugState(Drug.Paracetamol);
                paracetamol.AddEffect(fever, healthy);
                paracetamol.AddEffect(null, dead, aspirin);

            }
        }

    }
}
