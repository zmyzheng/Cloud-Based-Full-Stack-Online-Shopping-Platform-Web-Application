# Cloud Based Full Stack Online Shopping Platform Web Application
## E6998 Sec 6 Modern *-As-A-Service Application Development Project


### Overview
(TypeScript, Angular, Node.js, Bootstrap, jQuery, HTML/CSS, AWS Elastic Beanstalk, S3, AWS RDS, Stripe)

§ Developed an online shopping web application with Angular and Node.js based on microservice architecture

§ Realized authentication and authorization with OAuth 2.0 and simulated credit card transaction via Stripe

§ Design and implement RESTful APIs with Swagger for efficient and robust data exchange

§ Create high performance and flexible database schemas on AWS RDS Platform to record order and user information

### Requirement:
**FIRST: set up your ssh keys with [GitHub](https://help.github.com/articles/generating-a-new-ssh-key-and-adding-it-to-the-ssh-agent/)**

- [Typescript](http://typescriptlang.org)
  - Getting typings with [@types](https://www.npmjs.com/~types)
- [Node](http://nodejs.org)
  - Node Package Manager (npm)
- [Gulp](http://gulpjs.com)
  - gulpfile.js
- [Express](http://expressjs.com)
- [Angular](http://angular.io)
  - Version 2
  - [CLI](http://cli.angular.io)
  - [CLI Dev](https://github.com/angular/angular-cli)
- [Bootstrap](http://getbootstrap.com)
- Unit test: [TDD (Test Driven Develop)](https://en.wikipedia.org/wiki/Test-driven_development)
  - [Shim](https://en.wikipedia.org/wiki/Shim_(computing))
  - Framework to use:
    - Runner: [Karma](https://karma-runner.github.io/)
    <!--- Tester: [MochaJS](https://mochajs.org/)-->
- [MySQL](http://mysql.com)
  - ORM: [Dommel](https://github.com/henkmollema/Dommel)
- [RESTful API (Http call: GET POST PUT DELETE...)](https://en.wikipedia.org/wiki/Hypertext_Transfer_Protocol)
  - Testing Tool: 
    - Postman (in Chrome)
    - REST Client (in VSCode)
    - HttpRequester (in Sublime)
- [Webpack](https://webpack.github.io)
- [Travis CI](https://travis-ci.org)
- [Git](https://guides.github.com)
  - [Branch](https://git-scm.com/book/en/v1/Git-Branching-What-a-Branch-Is)
  - [Issues](https://guides.github.com/features/issues/)
  - [Fork](https://guides.github.com/activities/forking/)
  - [Pull Request](https://help.github.com/articles/about-pull-requests/)

### Coding Guideline
- [TypeScript](https://github.com/Microsoft/TypeScript/wiki/Coding-guidelines)
- [C#](https://msdn.microsoft.com/en-us/library/ff926074.aspx)
- [Node.js](https://nodejs.org/en/docs/guides/)
- [Express](https://expressjs.com/en/starter/installing.html)
- [JSON](https://google.github.io/styleguide/jsoncstyleguide.xml)
- [Unit Test](http://geosoft.no/development/unittesting.html)

### RESTful API Guideline
```
/Collections
  - GET
  - POST
    /{Parameters}
      - GET
      - PUT
      - DELETE
```
