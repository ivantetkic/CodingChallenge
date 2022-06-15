using HospitalSimulatorConsole.Infrastructure;

namespace HospitalSimulatorConsoleTest
{
    public class HospitalSimulatorServiceTest
    {

        [Theory]
        [InlineData("H,F,H", "P,I,As")]
        [InlineData("F,H", "P,As")]
        [InlineData("F", "P,I,As")]
        public void Patients_Should_Be_Dead(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            int noPatients = (_patients.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>()).Count();
            string expected = $"F:0,H:0,D:0,T:0,X:{noPatients}";

            Assert.Equal(expected, validationResult);
        }


        [Theory]
        [InlineData("F", "P,I")]
        [InlineData("F", "As")]
        [InlineData("F", "P,An")]
        [InlineData("H", "I,An,As")]
        public void Patients_Should_Be_Cured(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            int noPatients = (_patients.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>()).Count();
            string expected = $"F:0,H:{noPatients},D:0,T:0,X:0";

            Assert.Equal(expected, validationResult);
        }


        [Theory]
        [InlineData("F", "An")]
        [InlineData("F", "I")]
        [InlineData("H", "I,An")]
        public void Patients_Should_Have_Fiver(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            int noPatients = (_patients.Split(',').Select(x => x.Trim()).ToList() ?? new List<string>()).Count();
            string expected = $"F:{noPatients},H:0,D:0,T:0,X:0";

            Assert.Equal(expected, validationResult);
        }


        [Theory]
        [InlineData("D,D", "")]
        public void Patients_Sample_1(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            string expected = $"F:0,H:0,D:0,T:0,X:2";

            Assert.Equal(expected, validationResult);
        }

        [Theory]
        [InlineData("F", "P")]
        public void Patients_Sample_2(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            string expected = $"F:0,H:1,D:0,T:0,X:0";

            Assert.Equal(expected, validationResult);
        }

        [Theory]
        [InlineData("F", "As")]
        public void Patients_Sample_3(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            string expected = $"F:0,H:1,D:0,T:0,X:0";

            Assert.Equal(expected, validationResult);
        }

        [Theory]
        [InlineData("D", "I")]
        public void Patients_Sample_4(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            string expected = $"F:0,H:0,D:1,T:0,X:0";

            Assert.Equal(expected, validationResult);
        }

        [Theory]
        [InlineData("T", "An")]
        public void Patients_Sample_5(string _patients, string _drugs)
        {
            string validationResult = HospitalService.RunCheck(_patients, _drugs);

            string expected = $"F:0,H:1,D:0,T:0,X:0";

            Assert.Equal(expected, validationResult);
        }
    }
}