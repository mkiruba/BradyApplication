# BradyApplication

List of Projects
  - Brady.Console - Console App entry point for the application
  - Brady.Application - Class library for processing the xml and create output
      - Commands - Handle commands sent from Console ( In Production I would prefer completely seperated from caller using Mediator pattern)
      - Entities - Represet Xml objects
      - Services - Classes that read and write xml file (In Production this would be repoistories)
      - Strategies - Classes implementing Strategy pattern for different calculations
      
Best Practices

- Followed .Net guidelines for coding standards
- Dependency Injection
- Unit tests

Development Environment

- VS 2022/2019
- Framework .Net 6.0
