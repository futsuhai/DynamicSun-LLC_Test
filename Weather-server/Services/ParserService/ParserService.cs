using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Weather_server.Models.Backend;

namespace Weather_server.Services.ParserService
{
    public class ParserService() : IParserService
    {
        public async Task<IList<Weather>> ParseFile(IFormFile file)
        {
            List<Weather> weatherData = [];
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);
            memoryStream.Position = 0;

            var workbook = new XSSFWorkbook(memoryStream);

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);

                if(!IsValidTableFormat(sheet))
                {
                    return await Task.FromResult(weatherData);
                }
            }

            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);

                for (int j = 4; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);
                    if (row == null)
                        continue;

                    var localDate = DateTime.ParseExact((row.GetCell(0)?.ToString() ?? "") + " " + (row.GetCell(1)?.ToString() ?? ""), "dd.MM.yyyy HH:mm", null);
                    Weather weather = new()
                    {
                        Date = new(localDate.Year, localDate.Month, localDate.Date.Day, localDate.Hour, localDate.Minute, localDate.Second, DateTimeKind.Utc),
                        Temperature = float.TryParse(row.GetCell(2)?.ToString(), out float temperature) ? temperature : 0,
                        Humidity = float.TryParse(row.GetCell(3)?.ToString(), out float humidity) ? humidity : 0,
                        Td = float.TryParse(row.GetCell(4)?.ToString(), out float td) ? td : 0,
                        Pressure = int.TryParse(row.GetCell(5)?.ToString(), out int pressure) ? pressure : 0,
                        WindDirection = row.GetCell(6)?.ToString() ?? "",
                        WindSpeed = int.TryParse(row.GetCell(7)?.ToString(), out int windSpeed) ? windSpeed : 0,
                        Cloudy = float.TryParse(row.GetCell(8)?.ToString(), out float cloudy) ? cloudy : 0,
                        H = int.TryParse(row.GetCell(9)?.ToString(), out int h) ? h : 0,
                        VV = int.TryParse(row.GetCell(10)?.ToString(), out int vv) ? vv : 0,
                        WeatherCondition = row.GetCell(11)?.ToString() ?? ""
                    };
                    weatherData.Add(weather);
                }
            }


            return await Task.FromResult(weatherData);
        }

        private static bool IsValidTableFormat(ISheet sheet)
        {
            return sheet != null && sheet.GetRow(2)?.PhysicalNumberOfCells == 12;
        }
    }
}