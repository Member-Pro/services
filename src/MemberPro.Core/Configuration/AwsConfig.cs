namespace MemberPro.Core.Configuration
{
    public class AwsConfig
    {
        public string Region { get; set; }

        public string CognitoUrl { get; set; }

        public string CognitoAuthUrlBase { get; set; }

        public string UserPoolClientId { get; set; }

        public string UserPoolClientSecret { get; set; }

        public string SwaggerClientId { get; set; }

        public string SwaggerClientSecret { get; set; }
    }
}
