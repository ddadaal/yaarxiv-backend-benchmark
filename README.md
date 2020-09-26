# yaarxiv backend benchmark

This is a simple performance benchmark for alternative implementations for the same yaarxiv backend endpoint `/articles/{articleId}` ([swagger documentation](http://39.104.70.44:3000/swagger/static/index.html#/default/get_articles__articleId_)).

This API is a relatively complex query composed of the following three queries:

1. Query the `article` with param {articleId}
2. Query all the `revisions` of the article (one-to-many relation)
3. Query the relatest (or specified) `revision` of the article joined with the related `pdfUpload` (one-to-one relation).

All relations are implemented with **foreign constraints** for easier development.

The query design is not optimal: for example, there is no need in the 2nd step to query all revisions of article. There is also no manual fine-tuning of configurations, parameters etc. All these are default from the stack. This is also not a strict controlled benchmark.

Despite all the flaws, the results are still to some extent meaningful for performances of different techstacks.

Currently there is a [Spring Boot + Hibernate](springboot) and [ASP.NET Core + Entity Framework Core](aspnetcore) implementation.

# Current Result

## Environment

| Env | Description |
| -- | -- |
| PC | i7 9700 + 32GB 2666Mhz + SK Hynix BC511 512G NVMe SSD |
| OS | Windows 10 Pro 19041
| Database | MySQL 8.0.21 running inside a container with volumes mounted to local filesystem |
| `mysqldump` file  |[MySQL____localhost-2020_09_26_20_06_50-dump.sql](MySQL____localhost-2020_09_26_20_06_50-dump.sql) |
| Test command | `autocannon http://localhost:{port}/articles/2 -d 20` |

## How to read the result tables?

First, defeat your fear! The table and all its metrics are very intuitive.

But if you are too lazy to read these easy English words, the easiest metric is the average req/sec, i.e. the average requests per second. The bigger the number is, the faster the system performs.  

## Fastify + NodeJS

JWT Auth is implemented.

The built version is tested (i.e. not running directly with `ts-node`.)

| Component | Version |
| -- | -- |
| NodeJS | v12.18.4 |
| fastify | 3.4.1 |
| commit | [0564c4d](https://github.com/ddadaal/yaarxiv/commit/0564c4dd3b7df71e6d453eb91d6d7a2fcef521df) |

```
Running 20s test @ http://localhost:3000/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬─────────┬───────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev   │ Max   │
├─────────┼───────┼───────┼───────┼───────┼──────────┼─────────┼───────┤
│ Latency │ 11 ms │ 13 ms │ 21 ms │ 26 ms │ 13.95 ms │ 3.32 ms │ 78 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴─────────┴───────┘
┌───────────┬────────┬────────┬────────┬────────┬────────┬───────┬────────┐
│ Stat      │ 1%     │ 2.5%   │ 50%    │ 97.5%  │ Avg    │ Stdev │ Min    │
├───────────┼────────┼────────┼────────┼────────┼────────┼───────┼────────┤
│ Req/Sec   │ 400    │ 400    │ 730    │ 785    │ 693.2  │ 95.55 │ 400    │
├───────────┼────────┼────────┼────────┼────────┼────────┼───────┼────────┤
│ Bytes/Sec │ 239 kB │ 239 kB │ 436 kB │ 469 kB │ 414 kB │ 57 kB │ 239 kB │
└───────────┴────────┴────────┴────────┴────────┴────────┴───────┴────────┘

Req/Bytes counts sampled once per second.

14k requests in 20.04s, 8.28 MB read
```

## ASP.NET Core

JWT Auth is NOT implemented.

Release build is used.

App is running on local IIS Express server, not inside docker.

| Component | Version | 
| -- | -- |
| .NET Core | 3.1.402 |
| Entity Framework Core | 3.1.8 |
| MySql.Data.EntityFrameworkCore | 8.0.21 |


```
Running 20s test @ http://localhost:5000/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬─────────┬─────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev   │ Max     │
├─────────┼───────┼───────┼───────┼───────┼──────────┼─────────┼─────────┤
│ Latency │ 13 ms │ 15 ms │ 24 ms │ 28 ms │ 18.01 ms │ 64.3 ms │ 2139 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴─────────┴─────────┘
┌───────────┬─────┬──────┬────────┬────────┬────────┬─────────┬────────┐
│ Stat      │ 1%  │ 2.5% │ 50%    │ 97.5%  │ Avg    │ Stdev   │ Min    │
├───────────┼─────┼──────┼────────┼────────┼────────┼─────────┼────────┤
│ Req/Sec   │ 0   │ 0    │ 606    │ 641    │ 541.25 │ 184.9   │ 485    │
├───────────┼─────┼──────┼────────┼────────┼────────┼─────────┼────────┤
│ Bytes/Sec │ 0 B │ 0 B  │ 319 kB │ 337 kB │ 285 kB │ 97.2 kB │ 255 kB │
└───────────┴─────┴──────┴────────┴────────┴────────┴─────────┴────────┘

Req/Bytes counts sampled once per second.

11k requests in 20.07s, 5.69 MB read
```

## Spring Boot

JWT Auth is NOT implemented.

| Component | Version | 
| -- | -- |
| JDK | OpenJDK 11 |
| Spring Boot | 2.3.4 |

### Tomcat

```
Running 20s test @ http://localhost:8080/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬──────────┬────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev    │ Max    │
├─────────┼───────┼───────┼───────┼───────┼──────────┼──────────┼────────┤
│ Latency │ 13 ms │ 17 ms │ 41 ms │ 56 ms │ 20.17 ms │ 14.78 ms │ 412 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴──────────┴────────┘
┌───────────┬────────┬────────┬────────┬────────┬────────┬─────────┬────────┐
│ Stat      │ 1%     │ 2.5%   │ 50%    │ 97.5%  │ Avg    │ Stdev   │ Min    │
├───────────┼────────┼────────┼────────┼────────┼────────┼─────────┼────────┤
│ Req/Sec   │ 227    │ 227    │ 485    │ 627    │ 484.3  │ 101.56  │ 227    │
├───────────┼────────┼────────┼────────┼────────┼────────┼─────────┼────────┤
│ Bytes/Sec │ 123 kB │ 123 kB │ 262 kB │ 338 kB │ 261 kB │ 54.8 kB │ 123 kB │
└───────────┴────────┴────────┴────────┴────────┴────────┴─────────┴────────┘

Req/Bytes counts sampled once per second.

10k requests in 20.06s, 5.23 MB read
26 errors (0 timeouts)
```

### Netty

```
Running 20s test @ http://localhost:8080/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬──────────┬────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev    │ Max    │
├─────────┼───────┼───────┼───────┼───────┼──────────┼──────────┼────────┤
│ Latency │ 12 ms │ 15 ms │ 37 ms │ 44 ms │ 19.29 ms │ 17.42 ms │ 601 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴──────────┴────────┘
┌───────────┬─────────┬─────────┬────────┬────────┬────────┬─────────┬─────────┐
│ Stat      │ 1%      │ 2.5%    │ 50%    │ 97.5%  │ Avg    │ Stdev   │ Min     │
├───────────┼─────────┼─────────┼────────┼────────┼────────┼─────────┼─────────┤
│ Req/Sec   │ 164     │ 164     │ 517    │ 584    │ 506.3  │ 85.69   │ 164     │
├───────────┼─────────┼─────────┼────────┼────────┼────────┼─────────┼─────────┤
│ Bytes/Sec │ 71.9 kB │ 71.9 kB │ 227 kB │ 256 kB │ 222 kB │ 37.5 kB │ 71.8 kB │
└───────────┴─────────┴─────────┴────────┴────────┴────────┴─────────┴─────────┘

Req/Bytes counts sampled once per second.

10k requests in 20.08s, 4.44 MB read
```

# License

The implementations inside this repo are licensed under MIT. 

The original fastify + TypeScript implemented is licensed as specified in the original repo. 