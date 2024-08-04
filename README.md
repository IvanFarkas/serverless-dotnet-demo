## Lambda Demo with .NET

With the release of .NET 8 [AWS Lambda](https://aws.amazon.com/lambda/) now supports .NET 8 as managed runtimes. With the availability of ARM64 using Graviton3 there have been vast improvements to using .NET with Lambda.

But how does that translate to actual application performance? And how does .NET compare to other available runtimes. This repository contains a simple serverless application across a range of .NET implementations and the corresponding benchmarking results.

## Application

![](./imgs/diagram.jpg)

The application consists of an [Amazon API Gateway](https://aws.amazon.com/api-gateway/) backed by four Lambda functions and an [Amazon DynamoDB](https://aws.amazon.com/dynamodb/) table for storage.

It includes the below implementations as well as benchmarking results for both x86 and ARM64:

- .NET 8
- .NET 8 Native AOT
- .NET 8 Minimal API

## Requirements

- [AWS CLI](https://aws.amazon.com/cli/)
- [AWS SAM](https://aws.amazon.com/serverless/sam/)
- .NET 8
- [Artillery](https://www.artillery.io/) for load-testing the application
- Docker

## Software

There are four implementations included in the repository, covering a variety of Lambda runtimes and features. All the implementations use 1024MB of memory with Graviton3 (ARM64) as default. Tests are also executed against x86_64 architectures for comparison.

There is a separate project for each of the four Lambda functions, as well as a shared library that contains the data access implementations. It uses the hexagonal architecture pattern to decouple the entry points, from the main domain and storage logic.

## .NET 8

### .NET 8 Managed

The code is compiled for the .NET 8 AWS Lambda managed runtime. The code is compiled as ReadyToRun for cold start speed. This sample should be able to be tested with `sam build` and then `sam deploy --guided`. 

### .NET 8 native AOT

The code is compiled natively for Linux-x86_64 or ARM64 then deployed to Lambda as a zip file.

Details for compiling .NET native AOT can be found [here](https://github.com/dotnet/runtimelab/blob/feature/NativeAOT/docs/using-nativeaot/compiling.md)

### .NET 8 minimal API with native AOT

There is a single project named ApiBootstrap that contains all the start-up code and API endpoint mapping. The code is compiled natively for Linux-x86_64 then deployed manually to Lambda as a zip file. Microsoft have announced limited support for ASP.NET and native AOT in .NET 8, using the `WebApplication.CreateSlimBuilder(args);` method.

Details for compiling .NET 8 native AOT can be found [here](https://github.com/dotnet/runtimelab/blob/feature/NativeAOT/docs/using-nativeaot/compiling.md)

## Deployment

To deploy the architecture into your AWS account, navigate into the respective folder under the src folder and run 'sam deploy --guided'. This will launch a deployment wizard, complete the required values to initiate the deployment. For example, for .NET 6:

``` bash
cd src/NET6
sam build
sam deploy --guided
```

## Testing

Benchmarks are executed using [Artillery](https://www.artillery.io/). Artillery is a modern load testing & smoke testing library for SRE and DevOps.

To run the tests, use the below scripts. Replace the $API_URL with the API URL output from the deployment:

``` bash
cd loadtest
artillery run load-test.yml --target "$API_URL"
```

## Summary
Below is the cold start and warm start latencies observed. Please refer to the load test folder to see the specifics of the test that were executed.

All latencies listed below are in milliseconds.

 is used to make **100 requests / second for 10 minutes to our API endpoints**.

[AWS Lambda Power Tuning](https://github.com/alexcasalboni/aws-lambda-power-tuning) is used to optimize the cost/performance. 1024MB of function memory provided the optimal balance between cost and performance.

For the .NET 8 Native AOT compiled example the optimal memory allocation was 3008mb.

![](./imgs/power-tuning.PNG)

### Results

The below CloudWatch Log Insights query was used to generate the results:

```
filter @type="REPORT"
| fields greatest(@initDuration, 0) + @duration as duration, ispresent(@initDuration) as coldstart
| stats count(*) as count, pct(duration, 50) as p50, pct(duration, 90) as p90, pct(duration, 99) as p99, max(duration) as max by coldstart
```

### .NET 8

The .NET 8 benchmarks include the number of cold and warm starts, alongside the performance numbers. Typically, the cold starts account for 1% or less of the total number of invocations.

<table class="table-bordered">
        <tr>
            <th colspan="1" style="horizontal-align : middle;text-align:center;"></th>
            <th colspan="5" style="horizontal-align : middle;text-align:center;">Cold Start (ms)</th>
            <th colspan="5" style="horizontal-align : middle;text-align:center;">Warm Start (ms)</th>           
        </tr>
        <tr>
            <th></th>
            <th scope="col">Invoke Count</th>
            <th scope="col">p50</th>
            <th scope="col">p90</th>
            <th scope="col">p99</th>
            <th scope="col">max</th>
            <th scope="col">Invoke Count</th>
            <th scope="col">p50</th>
            <th scope="col">p90</th>
            <th scope="col">p99</th>
            <th scope="col">max</th>
        </tr>
        <tr>
            <th>x86_64</th>
            <td>1490</td>
            <td>860</td>
            <td>962</td>
            <td>1403</td>
            <td>1676</td>
            <td>45,436</td>
            <td><b style="color: green">6.1</b></td>
            <td><b style="color: green">10.7</b></td>
            <td><b style="color: green">27.7</b></td>
            <td>63.4</td>
        </tr>
        <tr>
            <th>ARM64</th>
            <td>1699</td>
            <td>1063</td>
            <td>1112</td>
            <td>1155</td>
            <td>1209</td>
            <td>45,093</td>
            <td><b style="color: green">6.6</b></td>
            <td><b style="color: green">14.6</b></td>
            <td><b style="color: green">30.8</b></td>
            <td>75.9</td>
        </tr>
        <tr>
            <th>x86_64 Native AOT</th>
            <td>758</td>
            <td>322</td>
            <td>344</td>
            <td>441</td>
            <td>665</td>
            <td>45,914</td>
            <td><b style="color: green">5.0</b></td>
            <td><b style="color: green">7.7</b></td>
            <td><b style="color: green">14.7</b></td>
            <td>77.0</td>
        </tr>
        <tr>
            <th>ARM64 Native AOT</th>
            <td>689</td>
            <td>334</td>
            <td>347</td>
            <td>372</td>
            <td>442</td>
            <td>646,081</td>
            <td><b style="color: green">5.3</b></td>
            <td><b style="color: green">7.9</b></td>
            <td><b style="color: green">13.4</b></td>
            <td>54.6</td>
        </tr>
        <tr>
            <th>ARM64 Native AOT Minimal API</th>
            <td>91</td>
            <td>498</td>
            <td>522</td>
            <td>895</td>
            <td>895</td>
            <td>156,359</td>
            <td><b style="color: green">5.6</b></td>
            <td><b style="color: green">8.8</b></td>
            <td><b style="color: green">16.1</b></td>
            <td>214.3</td>
        </tr>
</table>

**[Microsoft do not officially support all ASP.NET Core features for native AOT](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/), some features of ASP.NET may not be supported.*

Native AOT container samples use an Alpine base image. A cold start latency of ~1s was seen the first time an image was pushed and invoked. 

On future invokes, even after forcing new Lambda execution environments, cold start latency is as seen above. Potential reasons why covered in an [AWS blog post on optimizing Lambda functions packaged as containers.](https://aws.amazon.com/blogs/compute/optimizing-lambda-functions-packaged-as-container-images/)

## Security

See [CONTRIBUTING](CONTRIBUTING.md#security-issue-notifications) for more information.

## License

This library is licensed under the MIT-0 License. See the LICENSE file.
