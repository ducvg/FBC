pipeline {
  agent any

  stages {
    stage('Checkout') {
      steps {
        git branch: 'master', url: 'git@github.com:ducvg/FBC.git'
      }
    }
  }

  stage('Build') {
    steps {
      // Build the project
      sh 'dotnet --version'
    }
  }
}

post {
  always {
    cleanWs()
  }
}
