pipeline {
  agent any

  stages {
    stage('Checkout') {
      steps {
        git branch: 'master', url: 'https://github.com/ducvg/FBC.git'
         credentialsId: 'chu0jz013-github-token'
      }
    }
    stage('Build') {
      steps {
        // Build the project
        sh 'dotnet --version'
      }
    }
  }
}
