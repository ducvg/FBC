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
        sh 'tmux -V'

        catchError(buildResult: 'SUCCESS', stageResult: 'FAILURE') {
          sh 'tmux -S /tmp/tmux-1000/default kill-session -t fbc-session'
        }

        sh 'tmux -S /tmp/tmux-1000/default new-session -A -s fbc-session'
        sh 'dotnet publish/FBC.dll'
        sh 'tmux detach'
      }
    }
  }
}
