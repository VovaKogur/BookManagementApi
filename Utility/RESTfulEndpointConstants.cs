namespace Utility
{
    public static class RESTfulEndpointConstants
    {
        const string _BASEURL = "/api";

        public static class Books
        {
            const string _controllerName = nameof(Books);

            public const string ADD_BOOK = $"{_BASEURL}/{_controllerName}/add-book";
            public const string UPDATE = $"{_BASEURL}/{_controllerName}/update";
            public const string REMOVE_BY_ID = $"{_BASEURL}/{_controllerName}/delete";

        }

        public static class Reviews
        {
            const string _controllerName = nameof(Reviews);

        }

        public static class Categories
        {
            const string _controllerName = nameof(Categories);

            public const string ADD_CATEGORY = $"{_BASEURL}/{_controllerName}/add-category";
            public const string GET_CATEGORY = $"{_BASEURL}/{_controllerName}/get-category";
           
        }
    }
}
