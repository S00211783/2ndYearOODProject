﻿// <auto-generated />
namespace project.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.4.4")]
    public sealed partial class AddedUserIDToTransactions : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(AddedUserIDToTransactions));
        
        string IMigrationMetadata.Id
        {
            get { return "202304101203185_AddedUserIDToTransactions"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}