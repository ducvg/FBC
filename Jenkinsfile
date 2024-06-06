pipeline {
  agent any

    environment {
        GIT_CREDENTIALS_ID = 'chu0jz013-github-token'
    }

  stages {
    stage('Checkout') {
      steps {
        git credentialsId: env.GIT_CREDENTIALS_ID,
        url: 'https://github.com/ducvg/FBC.git',
        branch: 'master'
      }
    }
    stage('Build') {
      steps {
        sh 'dotnet --version'
        sh 'dotnet build'
        sh 'dotnet publish -c Release -o ./publish'
        sh 'ls -la'
      }
    }
    stage('Release') {
      steps {
        sh 'ls -la'
        script {
          sh '''
          nohup dotnet ./publish/FBC.dll &
          '''
        }
      }
    }
  }
}
