using FluentMigrator;

namespace FooService.DataAccess.Migrations
{
   [Migration(210204_1721)]
   public class M210204_1721_CreateObservationTable : UpOnlyMigration
   {
      public override void Up()
      {
         Create.Table("Observation")
            .WithColumn("Oid").AsGuid().NotNullable().PrimaryKey()
            .WithColumn("CreatedOn").AsDateTime2().NotNullable()
            .WithColumn("ModifiedOn").AsDateTime2().NotNullable()
            .WithColumn("ObservedOn").AsDateTime2().NotNullable()
            .WithColumn("Temperature").AsDouble().NotNullable()
            ;
      }
   }
}
