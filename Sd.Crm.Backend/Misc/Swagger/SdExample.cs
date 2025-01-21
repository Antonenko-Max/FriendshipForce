using Swashbuckle.AspNetCore.Filters;

namespace Sd.Crm.Backend.Misc.Swagger
{
    public class SdExample : IExamplesProvider<ClassToMakeSwaggerHappy>
    {
        public ClassToMakeSwaggerHappy GetExamples() => new ClassToMakeSwaggerHappy();

    }

    public class ClassToMakeSwaggerHappy
    {

    }
}
