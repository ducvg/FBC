pipeline {
  agent any

    environment {
        GIT_CREDENTIALS_ID = 'chu0jz013-github-token'
        DOCKER_IMAGE = 'fbc'
    }

  stages {
    stage('Checkout') {
      steps {
        git credentialsId: env.GIT_CREDENTIALS_ID,
        url: 'https://github.com/ducvg/FBC.git',
        branch: 'master'
      }
    }
    stage('Dotnet Build') {
      steps {
        sh 'dotnet --version'
        sh 'dotnet build'
        sh 'dotnet publish -c Release -o ./publish'
        sh 'ls -la'
      }
    }

    stage('Deploy') {
      steps {
        script {
          sh "echo 'root' | su -c 'pkill -f FBC'"
          sh "echo 'root' | su -c 'whoami'"
          sh "echo 'root' | su -c 'nohup dotnet publish/FBC.dll &'"
        }
        sh 'curl fbookcycle.store'
      }
    }
  }
}
