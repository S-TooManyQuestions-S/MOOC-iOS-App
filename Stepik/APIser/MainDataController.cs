
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stepik.APIser
{
    class MainDataController
    {
        [JsonProperty("meta")]//мета результаты содержащии пояснительную информацию
        public Meta metaresults { get; set; }
        [JsonProperty("search-results")]//основная информация
        public List<StepikCourse> search_results { get; set; }
        private bool _isFound = true;
        [JsonProperty("detail")]//если страницы нет
        public bool IsFound
        {
            get { return _isFound; }
            set { _isFound = value.ToString() == "Not Found" ? false : true; }
        }
    }
}
