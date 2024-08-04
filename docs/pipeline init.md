# Create GitHub Actions Workflow
[Details](https://catalog.workshops.aws/complete-aws-sam/en-US/module-4-cicd/module-4-cicd-gh/50-sampipeinit#create-github-actions-workflow)

```powershell
cd D:\Projects\AWS\serverless-dotnet-demo
# To specify details for an individual stage.
sam pipeline bootstrap
```

## Log

```powershell
​sam pipeline init generates a pipeline configuration file that your CI/CD system
can use to deploy serverless applications using AWS SAM.
We will guide you through the process to bootstrap resources for each stage,
then walk through the details necessary for creating the pipeline config file.

Please ensure you are in the root folder of your SAM application before you begin.

Select a pipeline template to get started:
        1 - AWS Quick Start Pipeline Templates
        2 - Custom Pipeline Template Location
Choice: 1


Cloning from https://github.com/aws/aws-sam-cli-pipeline-init-templates.git (process may take a moment)

Select CI/CD system
        1 - Jenkins
        2 - GitLab CI/CD
        3 - GitHub Actions
        4 - Bitbucket Pipelines
        5 - AWS CodePipeline
Choice: 3
You are using the 2-stage pipeline template.
 _________    _________
|         |  |         |
| Stage 1 |->| Stage 2 |
|_________|  |_________|

Checking for existing stages...

2 stage(s) were detected, matching the template requirements. If these are incorrect, delete .aws-sam/pipeline/pipelineconfig.toml and rerun

This template configures a pipeline that deploys a serverless application to a testing and a production stage.

What is the git branch used for production deployments? [main]:
What is the template file path? [template.yaml]:
We use the stage configuration name to automatically retrieve the bootstrapped resources created when you ran `sam pipeline bootstrap`.

Here are the stage configuration names detected in .aws-sam\pipeline\pipelineconfig.toml:
        1 - dev
        2 - prod
Select an index or enter the stage 1's configuration name (as provided during the bootstrapping): 1
What is the sam application stack name for stage 1? [sam-app]: LambdaAot-dev
Stage 1 configured successfully, configuring stage 2.

Here are the stage configuration names detected in .aws-sam\pipeline\pipelineconfig.toml:
        1 - dev
        2 - prod
Select an index or enter the stage 2's configuration name (as provided during the bootstrapping): 2
What is the sam application stack name for stage 2? [sam-app]: LambdaAot-prod
Stage 2 configured successfully.

SUMMARY
We will generate a pipeline config file based on the following information:
        Select a user permissions provider.: OpenID Connect (OIDC)
        What is the git branch used for production deployments?: main
        What is the template file path?: template.yaml
        Select an index or enter the stage 1's configuration name (as provided during the bootstrapping): 1
        What is the sam application stack name for stage 1?: LambdaAot-dev
        What is the pipeline execution role ARN for stage 1?: arn:aws:iam::533267171025:role/aws-sam-cli-managed-dev-pipel-PipelineExecutionRole-OERDlfTcOc4Y
        What is the CloudFormation execution role ARN for stage 1?: arn:aws:iam::533267171025:role/aws-sam-cli-managed-dev-p-CloudFormationExecutionRo-oaGN8KoA4NhI
        What is the S3 bucket name for artifacts for stage 1?: aws-sam-cli-managed-dev-pipeline-r-artifactsbucket-029k3mnnx0uy
        What is the ECR repository URI for stage 1?:
        What is the AWS region for stage 1?: us-east-1
        Select an index or enter the stage 2's configuration name (as provided during the bootstrapping): 2
        What is the sam application stack name for stage 2?: LambdaAot-prod
        What is the pipeline execution role ARN for stage 2?: arn:aws:iam::533267171025:role/aws-sam-cli-managed-prod-pipe-PipelineExecutionRole-AhRRPAdH11jL
        What is the CloudFormation execution role ARN for stage 2?: arn:aws:iam::533267171025:role/aws-sam-cli-managed-prod--CloudFormationExecutionRo-KZGfWSSz6Ods
        What is the S3 bucket name for artifacts for stage 2?: aws-sam-cli-managed-prod-pipeline--artifactsbucket-2hjoxphx4dp7
        What is the ECR repository URI for stage 2?:
        What is the AWS region for stage 2?: us-east-1
Successfully created the pipeline configuration file(s):
        - .github\workflows\pipeline.yaml
λ```
​