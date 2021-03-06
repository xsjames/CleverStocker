﻿namespace CleverStocker.Client.DockForms
{
    /// <summary>
    /// 最近交易
    /// </summary>
    public partial class RecentTradeForm : SingleToolDockForm// , IMQPubsubable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentTradeForm"/> class.
        /// </summary>
        public RecentTradeForm()
        {
            this.InitializeComponent();

            this.Icon = AppResource.RecentTradeIcon;
        }
    }
}
