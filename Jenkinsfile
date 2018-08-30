node {
    def app
    def branch

    stage('Clone repository') {
        checkout scm
        branch = env.BRANCH_NAME
    }

    stage('Build image') {
        app = docker.build("saltz/din")
    }

    stage('Push image') {
        if(branch == 'master' || branch == 'dev') {
            echo 'image will be pushed to dockerhub'
            docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
                if (branch == 'master') {
                  app.push("latest")
                  app.push("${env.BUILD_NUMBER}-latest-build")
                } else if (branch == 'dev') {
                  app.push("nightly")
                  app.push("${env.BUILD_NUMBER}-nightly-build")
                }
            }
        }
    }
}
