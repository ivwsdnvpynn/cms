﻿using System.Collections.Generic;
using Datory;
using SiteServer.Plugin;

namespace SiteServer.Cli.Core
{
    public class TableInfo
    {
        public List<DatoryColumn> Columns { get; set; }
        public int TotalCount { get; set; }
        public List<string> RowFiles { get; set; }
    }
}
