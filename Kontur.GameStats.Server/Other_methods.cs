
namespace Kontur.GameStats.Server
{
    class Other_methods
    {
        public static bool The_existence_check(bool endpoint_exist, string endpoint)
        {
            foreach (var e in WorkSQL.Select_Endpoint())
            {
                if (endpoint.Equals(e))
                {
                    endpoint_exist = true;
                    break;                   
                }
                else
                {
                    continue;
                }
            }
            return endpoint_exist;
        }


        public static bool The_existence_check(bool endpoint_exist, string endpoint, string timestap)
        {
            foreach (var e in WorkSQL.Select_Endpoint_timestamp())
            {
                if (endpoint.Equals(e[0]) && timestap.Equals(e[1]))
                {
                    endpoint_exist = true;
                    break;

                }
                else
                {
                    continue;
                }
            }
            return endpoint_exist;
        }

    }
}
