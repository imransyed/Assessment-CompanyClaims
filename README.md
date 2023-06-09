# CompanyClaims

My Solution for the API Assessment. The solution is using .Net 7 - minimal APIs.  FluentValidation used for DTO validation and xUnit for unit tests.

## Notes

There are two repositories, one for Companies and the other for Claims. The repositories are just static concurrent dictionaries - I wanted to get the datalayer built quickly without setting up EF or Dapper.  The repos are populated with random data in the static constructor.  I've added an endpoint to retrieve all claims - so you don't have to try and guess the claim details for use with the API.

All the repository methods are async - even though there are no underlying i/o operations.  The reason for this is because I would expect to use async heavily when accessing a real database.

I ignored the ClaimType table in the problem spec - as I wasn't 100% sure what it was for.  I assumed that UCR meant something like 'Unique Claims Reference' so in the absense of an explicit claims id on the claims table I'm using this as the primary key for the table and as a result the UCR is also used to look up a specific claim.

I've assumed that the requirement for a 'need a property to be returned that will tell us if the company has an active insurance policy' is met by simply returning the bit from the 'Active' column and no further logic needs to be applied to if there are active policies against a company.

As the problem specification is very light on business logic I've left the solution as two-layers - the web-service layer and the data layer.  It would have been overkill to have a specific business logic layer.  The database 'tables' are mapped to two entities - Company and Claim.  They are exposed at the endpoints via two DTOs - CompanyDto and ClaimDto.

For the update claim endpoint I've used fluent validation to apply some rudimentary validation to the incoming data.  I've assumed that the date of loss is the date some incident that resulted in a claim occurred and the claim date is the date the claim was reported to the insurer.  Hence, I've added validation to ensure that the loss date precedes the claim date.

I've done a few unit tests on the Update claim end point - they are by no means exhaustive.  With more time I would have tested all the endpoints as well as the validation rules.  The errors for the validation rules are hard-coded - and I would usually take those out into separate string constants to allow me to reference them in tests etc. as well as to make sure they're consistent.

## Missing

In a full solution there are a couple of things that I would have included.

The first is some Authentication/Authorisation on the different methods - this assuming that the API is deployed to the wider web and isn't published internally or behind an API Gateway or something.

Second, I added a 'Company Name' property to the claim for informational purposes - I think this would have been better as a URL to the actual company record on the /company/{id} end point.  I would have also done this in the 'get all claims' end point which supports paging to give the URL for the last and previous pages.  In fact both the endpoints that return lists I would have wrapped in a containing object so I could add some meta-data about the results returned, e.g. page information and total records returned / available.

Third, I've tried to only implement the endpoints required for the solution - I've not implemented any CREATE endpoints.  Just a note on this I wouldn't generally expose an API as simple CRUD operations - I would want the API to reflect actual business flows.  For example, I wouldn't usually have a blanket claims update - but several endpoints that would do operations like 'Settle Claim'.  The consumer of the API would then provide only pertinent information to complete that transaction - such as settlement amount, settlement date and a comment.  In a similar vein, the CREATE Claim operation would actually be something like a notification of loss operation rather than a straightforward DB insert.
