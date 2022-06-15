using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalSimulatorConsole.Infrastructure.Constants
{
    /// <summary>
    ///     This class contains available patients that can be used in service.It also contains the list with all patients.
    ///     Here you can add new patients if needs and add it should be added to the GetAll list.
    /// </summary>
    public static class Patient
    {

        public const string Fever = "F";
        public const string Healthy = "H";
        public const string Diabetes = "D";
        public const string Tuberculosis = "T";
        public const string Dead = "X";

        public static List<string> GetAll = new List<string> { Fever, Healthy, Diabetes, Tuberculosis, Dead };
    }
}
