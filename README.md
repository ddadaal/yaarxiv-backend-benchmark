# yaarxiv backend benchmark

This is a simple performance benchmark for alternative implementations for the same yaarxiv backend endpoint `/articles/{articleId}` ([swagger documentation](http://39.104.70.44:3000/swagger/static/index.html#/default/get_articles__articleId_)).

This API is a relatively complex query composed of the following three queries:

1. Query the `article` with param {articleId}
2. Query the `revisionNumber` and `time` of all the `revisions` of the article (one-to-many relation)
3. Query the relatest (or specified) `revision` of the article joined with the related `pdfUpload` (one-to-one relation).

All relations are implemented with **foreign constraints** for easier development.

The query design is not optimal. There is no manual fine-tuning of configurations, parameters etc. All these are default from the stack. This is also not a strict controlled benchmark.

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
| commit | [a8ae90a](https://github.com/ddadaal/yaarxiv/commit/a8ae90a817e56d0e9f6017d0e1ba2d2d53abbcb6) |

```
Running 20s test @ http://localhost:3000/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬─────────┬────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev   │ Max    │
├─────────┼───────┼───────┼───────┼───────┼──────────┼─────────┼────────┤
│ Latency │ 11 ms │ 13 ms │ 21 ms │ 25 ms │ 13.74 ms │ 3.65 ms │ 105 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴─────────┴────────┘
┌───────────┬────────┬────────┬────────┬────────┬────────┬─────────┬────────┐
│ Stat      │ 1%     │ 2.5%   │ 50%    │ 97.5%  │ Avg    │ Stdev   │ Min    │
├───────────┼────────┼────────┼────────┼────────┼────────┼─────────┼────────┤
│ Req/Sec   │ 390    │ 390    │ 738    │ 808    │ 702.85 │ 102.68  │ 390    │
├───────────┼────────┼────────┼────────┼────────┼────────┼─────────┼────────┤
│ Bytes/Sec │ 233 kB │ 233 kB │ 441 kB │ 483 kB │ 420 kB │ 61.3 kB │ 233 kB │
└───────────┴────────┴────────┴────────┴────────┴────────┴─────────┴────────┘

Req/Bytes counts sampled once per second.

14k requests in 20.04s, 8.39 MB read
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

┌─────────┬───────┬───────┬───────┬───────┬──────────┬──────────┬─────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev    │ Max     │
├─────────┼───────┼───────┼───────┼───────┼──────────┼──────────┼─────────┤
│ Latency │ 13 ms │ 16 ms │ 27 ms │ 32 ms │ 19.11 ms │ 69.75 ms │ 2245 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴──────────┴─────────┘
┌───────────┬─────┬──────┬────────┬────────┬────────┬─────────┬────────┐
│ Stat      │ 1%  │ 2.5% │ 50%    │ 97.5%  │ Avg    │ Stdev   │ Min    │
├───────────┼─────┼──────┼────────┼────────┼────────┼─────────┼────────┤
│ Req/Sec   │ 0   │ 0    │ 574    │ 620    │ 510.95 │ 176.82  │ 395    │
├───────────┼─────┼──────┼────────┼────────┼────────┼─────────┼────────┤
│ Bytes/Sec │ 0 B │ 0 B  │ 317 kB │ 343 kB │ 283 kB │ 97.8 kB │ 218 kB │
└───────────┴─────┴──────┴────────┴────────┴────────┴─────────┴────────┘

Req/Bytes counts sampled once per second.

10k requests in 20.08s, 5.65 MB read
```

## Spring Boot + Hibernate JPA

JWT Auth is NOT implemented.

| Component | Version | 
| -- | -- |
| JDK | OpenJDK 11 |
| Spring Boot | 2.3.4 |

### Tomcat

```
Running 20s test @ http://localhost:8080/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬─────────┬────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev   │ Max    │
├─────────┼───────┼───────┼───────┼───────┼──────────┼─────────┼────────┤
│ Latency │ 13 ms │ 16 ms │ 33 ms │ 41 ms │ 17.64 ms │ 13.5 ms │ 413 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴─────────┴────────┘
┌───────────┬────────┬────────┬────────┬────────┬────────┬────────┬────────┐
│ Stat      │ 1%     │ 2.5%   │ 50%    │ 97.5%  │ Avg    │ Stdev  │ Min    │
├───────────┼────────┼────────┼────────┼────────┼────────┼────────┼────────┤
│ Req/Sec   │ 246    │ 246    │ 577    │ 659    │ 551.4  │ 111.05 │ 246    │
├───────────┼────────┼────────┼────────┼────────┼────────┼────────┼────────┤
│ Bytes/Sec │ 140 kB │ 140 kB │ 328 kB │ 375 kB │ 313 kB │ 63 kB  │ 140 kB │
└───────────┴────────┴────────┴────────┴────────┴────────┴────────┴────────┘

Req/Bytes counts sampled once per second.

11k requests in 20.05s, 6.26 MB read
28 errors (0 timeouts)
```

### Netty

```
Running 20s test @ http://localhost:8080/articles/2
10 connections

┌─────────┬───────┬───────┬───────┬───────┬──────────┬──────────┬────────┐
│ Stat    │ 2.5%  │ 50%   │ 97.5% │ 99%   │ Avg      │ Stdev    │ Max    │
├─────────┼───────┼───────┼───────┼───────┼──────────┼──────────┼────────┤
│ Latency │ 11 ms │ 14 ms │ 33 ms │ 40 ms │ 17.91 ms │ 16.75 ms │ 565 ms │
└─────────┴───────┴───────┴───────┴───────┴──────────┴──────────┴────────┘
┌───────────┬─────────┬─────────┬────────┬────────┬────────┬───────┬─────────┐
│ Stat      │ 1%      │ 2.5%    │ 50%    │ 97.5%  │ Avg    │ Stdev │ Min     │
├───────────┼─────────┼─────────┼────────┼────────┼────────┼───────┼─────────┤
│ Req/Sec   │ 204     │ 204     │ 567    │ 607    │ 544.1  │ 85.93 │ 204     │
├───────────┼─────────┼─────────┼────────┼────────┼────────┼───────┼─────────┤
│ Bytes/Sec │ 95.1 kB │ 95.1 kB │ 264 kB │ 283 kB │ 254 kB │ 40 kB │ 95.1 kB │
└───────────┴─────────┴─────────┴────────┴────────┴────────┴───────┴─────────┘

Req/Bytes counts sampled once per second.

11k requests in 20.06s, 5.07 MB read
```

# License

The implementations inside this repo are licensed under MIT. 

The original fastify + TypeScript implemented is licensed as specified in the original repo. 