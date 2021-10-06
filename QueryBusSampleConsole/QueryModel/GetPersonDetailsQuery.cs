using System.ComponentModel.DataAnnotations;
using QueryBusSampleConsole.Framework;
using QueryBusSampleConsole.Framework.Base;

namespace QueryBusSampleConsole.QueryModel
{
    public class GetPersonDetailsQuery : IQuery<PersonQueryResult>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام اجباری می باشد")]
        public string Title { get; set; }
    }
}