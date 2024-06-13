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
          try {
            timeout(time: 5, unit: 'SECONDS') {
              sh "echo 'root' | su -c 'pkill -f FBC &'"
            }
          } catch (e) {
            echo "pkill command timed out: ${e}"
          }
          sh 'curl fbookcycle.store'
          sh "echo 'root' | su -c 'whoami'"
          sh "echo 'root' | su -c 'nohup dotnet publish/FBC.dll &'"
          sh 'curl fbookcycle.store'
          sleep 1
        }

        // script {
        //   sh "echo 'root' | su -c 'whoami'"
        //   sh "echo 'root' | su -c 'bash run.sh'"
        // }
        sh 'curl fbookcycle.store'
      }
    }
  }
}
