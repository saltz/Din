node {
    def app

    stage('Clone repository') {
        checkout scm
    }

    stage('Build image') {
        app = docker.build("saltz/din")
    }

    stage('Push image') {
        docker.withRegistry('https://registry.hub.docker.com', 'docker-hub-credentials') {
            app.push("${env.BUILD_NUMBER}")
            if (env.BRANCH_NAME == 'master') {
              app.push("latest")
            } else if (env.BRANCH_NAME == 'dev') {
              app.push("nightly")
            }
        }
    }
}
