﻿using System.Collections.Generic;
using Datory;
using Newtonsoft.Json;
using SiteServer.Plugin;

namespace SiteServer.Cli.Updater.Tables.GovInteract
{
    public partial class TableGovInteractPermissions
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("nodeID")]
        public long NodeId { get; set; }

        [JsonProperty("permissions")]
        public string Permissions { get; set; }
    }

    public partial class TableGovInteractPermissions
    {
        public const string OldTableName = "wcm_GovInteractPermissions";

        public static ConvertInfo Converter => new ConvertInfo
        {
            NewTableName = NewTableName,
            NewColumns = NewColumns,
            ConvertKeyDict = ConvertKeyDict,
            ConvertValueDict = ConvertValueDict
        };

        private static readonly string NewTableName = "ss_govinteract_permissions";

        private static readonly List<DatoryColumn> NewColumns = new List<DatoryColumn>
        {
            new DatoryColumn
            {
                AttributeName = "Id",
                DataType = DataType.Integer,
                IsPrimaryKey = true,
                IsIdentity = true
            },
            new DatoryColumn
            {
                AttributeName = "UserName",
                DataType = DataType.VarChar
            },
            new DatoryColumn
            {
                AttributeName = "ChannelId",
                DataType = DataType.Integer
            },
            new DatoryColumn
            {
                AttributeName = "Permissions",
                DataType = DataType.Text
            }
        };

        private static readonly Dictionary<string, string> ConvertKeyDict =
            new Dictionary<string, string>
            {
                {"ChannelId", nameof(NodeId)}
            };

        private static readonly Dictionary<string, string> ConvertValueDict = null;
    }
}
