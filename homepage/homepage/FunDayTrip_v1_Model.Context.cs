﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace homepage
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DB_FunDayTripEntities_v1 : DbContext
    {
        public DB_FunDayTripEntities_v1()
            : base("name=DB_FunDayTripEntities_v1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tAd> tAds { get; set; }
        public virtual DbSet<tAdmin> tAdmins { get; set; }
        public virtual DbSet<tAlbum> tAlbums { get; set; }
        public virtual DbSet<tBalance> tBalances { get; set; }
        public virtual DbSet<tBank> tBanks { get; set; }
        public virtual DbSet<tBlock> tBlocks { get; set; }
        public virtual DbSet<tComment> tComments { get; set; }
        public virtual DbSet<tCoordinate> tCoordinates { get; set; }
        public virtual DbSet<tFollow> tFollows { get; set; }
        public virtual DbSet<tFuntionAuth> tFuntionAuths { get; set; }
        public virtual DbSet<tGame> tGames { get; set; }
        public virtual DbSet<tGameGroup> tGameGroups { get; set; }
        public virtual DbSet<tGameQA> tGameQAs { get; set; }
        public virtual DbSet<tGameStep> tGameSteps { get; set; }
        public virtual DbSet<tGameUnlock> tGameUnlocks { get; set; }
        public virtual DbSet<tIcon> tIcons { get; set; }
        public virtual DbSet<tLocation> tLocations { get; set; }
        public virtual DbSet<tLR_Relation> tLR_Relation { get; set; }
        public virtual DbSet<tMember> tMembers { get; set; }
        public virtual DbSet<tMessage> tMessages { get; set; }
        public virtual DbSet<tNote> tNotes { get; set; }
        public virtual DbSet<tPhoto> tPhotoes { get; set; }
        public virtual DbSet<tPoint> tPoints { get; set; }
        public virtual DbSet<tProfitMember> tProfitMembers { get; set; }
        public virtual DbSet<tProfitType> tProfitTypes { get; set; }
        public virtual DbSet<tReport> tReports { get; set; }
        public virtual DbSet<tRole> tRoles { get; set; }
        public virtual DbSet<tRoute> tRoutes { get; set; }
        public virtual DbSet<tShareAuth> tShareAuths { get; set; }
        public virtual DbSet<tStatistic> tStatistics { get; set; }
        public virtual DbSet<tTag> tTags { get; set; }
        public virtual DbSet<tTagMsgRelation> tTagMsgRelations { get; set; }
        public virtual DbSet<tUnlock> tUnlocks { get; set; }
    }
}
