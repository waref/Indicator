namespace Depences.Infrastructure.Static
{
    public static class DbConstants
    {
        #region Connection Configuration
        public const string DefaultConnectionString = "DefaultConnectionString";
        #endregion

        #region Schemas Names
        #region Depences Module
        public const string DepenceSchemaName = "DEP";
        #endregion
        #endregion

        #region Tables Names

        #region Depences Module
        public const string DepenceTableName = "Depences";
        public const string UserTableName = "Users";
        public const string NatureTableName = "Natures";
        public const string DeviseTableName = "Devises";
        public const string UserDevisTableName = "UserDevis";
        #endregion

        #endregion

    }
}
