pipeline {

  agent any
  
  stages {
    stage('Checkout') {
      steps {
        // Checkout the code from the Git repository
        git branch: 'master', url: 'git@github.com:ducvg/FBC.git'
      }
    }
  }

  stage('Build') {
    steps {
      // Build the project
      sh 'dotnet build --configuration Release'
    }
  }

  stage('Run') {
    steps {
      // Run the application
      sh 'dotnet run --project your-project-name'
    }
  }
}

post {
  always {
    // Clean up after the build
    cleanWs()
  }
}