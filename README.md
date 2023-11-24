# TestMandiry-TicketAPI
Simple Microservices Order Tickets

# create Database
Host=localhost;
Port=5432;
Database=Test_Mandiri;
Username=postgres;
Password=123456;

# migrate Database
add migration
- PM> Add-Migration InitialCreate

update migration
- PM> Update-Database / PM> Update-Database -TargetMigration:"Migration Name"



