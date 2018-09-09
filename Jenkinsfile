node {
    def app
    def branch

    stage('Clone repository') {
        checkout scm
        branch = env.BRANCH_NAME
        echo 'Successful clone'
    }

    stage('Build image') {
        app = docker.build("saltz/din")
        echo 'Successful build'
    }

    stage('Push image') {
        if(branch == 'master' || branch == 'dev') {
            echo 'This is a release build'
            docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
                if (branch == 'master') {
                  app.push("latest")
                  echo 'latest release'
                } else if (branch == 'dev') {
                  app.push("nightly")
                  echo 'nightly release'
                }
            }
        }
        else {
          echo 'This build will not be released'
        }
    }
    echo 'Everything went smoothly :)'
}
