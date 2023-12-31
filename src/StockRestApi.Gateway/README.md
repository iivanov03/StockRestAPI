# StockRestApi.Gateway

The gateway microservice is the only microservice with exported ports in the docker-compose configuration. Its main responsibility is to re-route all of the requests to their desired microservice, while checking for things like authentication.

## Configuration
All of the configurations are stored under the `appsettings.json` (for 'prod') or the `appsettings.Development.json` file (for dev/local).
### Routes
All of the available routes with their access modifier and available Http methods are configured under the `Routes` section. This is basically an array of `Route` objects. \
\
Each `Route` object consists of mainly 3 properties : `Url`, `Access`, `Methods`.
1. `Url` - The url property is used for defining the pattern that the url must follow. For example if we want to match the `/api/user/123` url, we need to set this property to `exact: /user/:id`. *NOTE: In the routes we are not adding the `/api` prefix.*
2. `Access` - This property defines if the link is `Private`, `Public` or `Anonymous`.
   - `Private` - Only **authenticated** users will be able to access it.
   - `Public` - Both **authenticated** and **unauthenticated** users will be able to access it.
   - `Anonymous` - Only **unauthenticated** users will be able to access it.
3. `Methods` - This property defines which http methods are allowed. The following are available: `GET`, `PUT`, `POST` `DELETE`.
4. `Host` - This is the hostname of the microservice.
   - `appsettings.json` - this is basically the 'prod' version of the settings, here we will set the property to the name of the container.
   - `appsettings.Development.json` - this is basically the 'dev/local' version of the settings. Here we will set the property to the local ip address of the running local server. This makes debugging easier.