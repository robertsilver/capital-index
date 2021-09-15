# capital-index
- All code resides in the "development" branch.
- Task 1, section 3, states that the developer should be efficient in the number of requests they make to the API because the free account is limited to 1000 requests.  The most efficient call to make would be to use their time series API, however, as noted below, the free account is not allowed to make access to this API because "Access Restricted - Your current Subscription Plan does not support this API Function.".  The response is below.
http://data.fixer.io/api/timeseries?access_key=[SECRET]&start_date=2019-10-01&end_date=2019-10-31&base=EUR
success	false
error	
code	105
type	"function_access_restricted"
info	"Access Restricted - Your current Subscription Plan does not support this API Function."

Instead, the historical API must be used for this exercise.