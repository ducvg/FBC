pipeline {
  agent any

    environment {
        GIT_CREDENTIALS_ID = 'chu0jz013-github-token'
    }

  stages {
    stage('Checkout') {
      steps {
        git credentialsId: env.GIT_CREDENTIALS_ID, url: 'https://github.com/ducvg/FBC.git', branch: 'master'
      }
    }
    stage('Build') {
      steps {
        sh 'dotnet --version'
      }
    }
  }
}
