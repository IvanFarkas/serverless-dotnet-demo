# serverless-dotnet-demo notes

## Set GitHub variables and secrets in local environment variables

Request secrets from project manager.

```powershell
[Environment]::SetEnvironmentVariable('Git_Username', 'GitHub Action', 'User')
[Environment]::SetEnvironmentVariable('Git_Email', 'action@github.com', 'User')
[Environment]::SetEnvironmentVariable('GH_PKG_USER_IVAN', 'IvanFarkas', 'User')
[Environment]::SetEnvironmentVariable('GH_PKG_TOKEN', 'ghp_XXX...', 'User')
[Environment]::SetEnvironmentVariable('AWS_REGION', 'us-east-1', 'User')
[Environment]::SetEnvironmentVariable('AWS_ACCESS_KEY_ID', 'secret', 'User')
[Environment]::SetEnvironmentVariable('AWS_SECRET_ACCESS_KEY', 'secret', 'User')
[Environment]::SetEnvironmentVariable('AWS_ROLE', 'secret', 'User')
[Environment]::SetEnvironmentVariable('API_ENDPOINT_NET_8_NATIVE_X86', 'https://nativearmn.execute-api.us-east-1.amazonaws.com', 'User')
[Environment]::SetEnvironmentVariable('API_ENDPOINT_NET_8_NATIVE_ARM', 'https://nativearmy.execute-api.us-east-1.amazonaws.com', 'User')
```

## GitHub CLI Login

```powershell
$Env:GH_PKG_TOKEN | gh auth login --with-token
```

## Setup Project

```powershell
cd D:\Projects\AWS\Lambda\LambdaAot
git remote add origin git@github.com:IvanFarkas/serverless-dotnet-demo.git
git branch -M main

gh variable set GH_PKG_USER --body "$Env:GH_PKG_USER_IVAN" -R IvanFarkas/serverless-dotnet-demo
gh variable set GIT_EMAIL --body "$Env:Git_Email" -R IvanFarkas/serverless-dotnet-demo
gh variable set GIT_USERNAME --body "$Env:Git_Username" -R IvanFarkas/serverless-dotnet-demo
gh secret set AWS_REGION --body "$Env:AWS_REGION" -R IvanFarkas/serverless-dotnet-demo
gh secret set AWS_ACCESS_KEY_ID --body "$Env:AWS_ACCESS_KEY_ID" -R IvanFarkas/serverless-dotnet-demo
gh secret set AWS_SECRET_ACCESS_KEY --body "$Env:AWS_SECRET_ACCESS_KEY" -R IvanFarkas/serverless-dotnet-demo
gh secret set AWS_ROLE --body "$Env:AWS_ROLE" -R IvanFarkas/serverless-dotnet-demo
gh secret set API_ENDPOINT_NET_8_NATIVE_X86 --body "$Env:API_ENDPOINT_NET_8_NATIVE_X86" -R IvanFarkas/serverless-dotnet-demo
gh secret set API_ENDPOINT_NET_8_NATIVE_ARM --body "$Env:API_ENDPOINT_NET_8_NATIVE_ARM" -R IvanFarkas/serverless-dotnet-demo

# dotnet new tool-manifest --force
# dotnet tool install Husky
# dotnet husky install
```
