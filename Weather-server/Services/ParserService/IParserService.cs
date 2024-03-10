using Weather_server.Models.Backend;

namespace Weather_server.Services.ParserService
{
    public interface IParserService
    {
        public Task<IList<Weather>> ParseFiles(IFormFileCollection files);

    }
}