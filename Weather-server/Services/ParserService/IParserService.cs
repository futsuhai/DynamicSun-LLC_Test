using Weather_server.Models.Backend;

namespace Weather_server.Services.ParserService
{
    public interface IParserService
    {
        public Task<IList<Weather>> ParseFile(IFormFile file);

    }
}