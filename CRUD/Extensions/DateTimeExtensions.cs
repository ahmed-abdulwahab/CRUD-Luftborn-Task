namespace CRUD.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalaculateAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var age = today.Year - dob.Year;

            if (dob > today.AddYears(age - age)) --age;
            return age;
        }
    }
}
