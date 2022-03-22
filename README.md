# Digit classifier web app SaaS
Team project, this repo mirrors [this](https://bit.ly/3DgdNuN) one over on GitLab.

## Summary
A web software solution that provides the following features:
* Allows users to draw a digit into a canvas and it predicts that digit using a ML.NET classifier.
* Tracks history for every logged in user, including a photo of the drawing and the prediction result of the algorithm.
* Allows adding predictions from history to favorites.
* Shows all drawing made by users in a Global tab from where any user can give ratings stars.
* These drawings are filtered by rating & rating count and added to the dataset of the ML model to improve accuracy (extending a MNIST-like dataset).

## Demo
A *short* summary of all features is presented in the video below.
If you want to try the app yourself you can access [this](https://bit.ly/3itp0yd) deployed version which
may be slow at first operation due to it being hosted on a free development deployment slot,
app having to cold-start.


See the video below:

[![Demo](https://i.imgur.com/8S46BRJ.png)](https://bit.ly/3uh9cnJ "Digit Classifier")

Alt: [click](https://bit.ly/3uh9cnJ)

## Technologies

### Back end
- Web API using ASP .NET Core 5.
- SQL Server (locally) and Postgres SQL (production) with Entity Framework 6 for Persistance Layer.

### ML
- ML.NET algorithm for Multiclass classification, ML WebAPI for utilizing trained model and relaying data back.

### Front end
- Angular 13.
- Angular Material UI + Bootstrap.

## Architecture
* Web API's hosted on Heroku using docker containers
* Angular site hosted on Heroku using docker container
* Heroku Postgres for main web api.

### Back end (main api)
Organized as a N-Tier architecture solution. 

* Application layer:
  - entities.
  - DTOs.
  - exceptions.
  - helpers.
  - *services interfaces and their implementation*.
  - *exposes infrastracture contracts (email providers interfaces, persistance layer repositories interfaces)*.
  - AutoMapper for swapping contracts between DB / models.

* DataAccess (persistence) layer:
  - EF 6
  - Repository pattern.
  - Utilizes repository pattern for accesing resources grouped by the DB Relation it queries/modifies.

* API Presentation layer(StocksProcessing.API):
  - Uses JWT for Authorization / Authentication.
  - API Versioning.
  - ProblemDetails exceptions middleware.
  - Middleware for pulling user out of HttpContext by JWT token.

### ML (ML api)
* Uses SdcaMaximumEntropyMulticlassTrainer algorithm.
* Exposes a route that loads the model and creates a prediction engine and sends back the prediction result.
* Used by the 'Back end' (main api) internally.

### Front end
Using a highly scalable standard folder structure.
* Core consists of Services, Interceptors, Directives, Pipes.
* Shared module for models and common components.
* All specific components are organized in modules.
  - lazy loading modules for reducing bundle size.
