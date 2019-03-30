﻿using System;

namespace Datory.Tests.Mocks
{
    [Table("TestTable")]
    public class TestTableInfo : DynamicEntity
    {
        [TableColumn(Length = 100)]
        public string VarChar100 { get; set; }

        [TableColumn(Length = 100)]
        public string VarCharDefault { get; set; }

        [TableColumn(Text = true)]
        public string Content { get; set; }

        [TableColumn]
        public int Num { get; set; }

        [TableColumn]
        public decimal Currency { get; set; }

        [TableColumn]
        public DateTime? Date { get; set; }

        [TableColumn]
        private string IsLockedOut { get; set; }

        public bool Locked
        {
            get => DatoryUtils.ToBool(IsLockedOut);
            set => IsLockedOut = value.ToString();
        }
    }
}
