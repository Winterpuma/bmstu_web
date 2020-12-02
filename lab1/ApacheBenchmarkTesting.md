# Доступ к апи сервера напрямую
```
puma@LAPTOP:~$ ab -c 10 -n 10000 http://127.0.0.1:44305/api/gameboard
This is ApacheBench, Version 2.3 <$Revision: 1843412 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking 127.0.0.1 (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:
Server Hostname:        127.0.0.1
Server Port:            44305

Document Path:          /api/gameboard
Document Length:        0 bytes

Concurrency Level:      10
Time taken for tests:   7.032 seconds
Complete requests:      10000
Failed requests:        0
Total transferred:      0 bytes
HTML transferred:       0 bytes
Requests per second:    1421.98 [#/sec] (mean)
Time per request:       7.032 [ms] (mean)
Time per request:       0.703 [ms] (mean, across all concurrent requests)
Transfer rate:          0.00 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.0      0       1
Processing:     0    1   0.1      1       1
Waiting:        0    0   0.0      0       0
Total:          0    1   0.1      1       2

Percentage of the requests served within a certain time (ms)
  50%      1
  66%      1
  75%      1
  80%      1
  90%      1
  95%      1
  98%      1
  99%      1
 100%      2 (longest request)
 ```
 
 # Доступ к апи сервера напрямую, когда развернуто на 3х портах
```
puma@LAPTOP:/etc/nginx$ ab -c 10 -n 10000 http://localhost:44001/api/gameboard
This is ApacheBench, Version 2.3 <$Revision: 1843412 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:
Server Hostname:        localhost
Server Port:            44001

Document Path:          /api/gameboard
Document Length:        0 bytes

Concurrency Level:      10
Time taken for tests:   45.551 seconds
Complete requests:      10000
Failed requests:        0
Total transferred:      0 bytes
HTML transferred:       0 bytes
Requests per second:    219.53 [#/sec] (mean)
Time per request:       45.551 [ms] (mean)
Time per request:       4.555 [ms] (mean, across all concurrent requests)
Transfer rate:          0.00 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.1      0       1
Processing:     3    4   1.0      4      36
Waiting:        0    0   0.0      0       0
Total:          3    4   1.0      4      36

Percentage of the requests served within a certain time (ms)
  50%      4
  66%      5
  75%      5
  80%      5
  90%      5
  95%      6
  98%      7
  99%      7
 100%     36 (longest request)
```

 
 # Без балансировки
```
puma@LAPTOP:/etc/nginx$ ab -l -c 10 -n 10000 http://localhost:2345/api/v1/gameboard
This is ApacheBench, Version 2.3 <$Revision: 1843412 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:        snake
Server Hostname:        localhost
Server Port:            2345

Document Path:          /api/v1/gameboard
Document Length:        Variable

Concurrency Level:      10
Time taken for tests:   6.683 seconds
Complete requests:      10000
Failed requests:        0
Non-2xx responses:      10000
Total transferred:      4970000 bytes
HTML transferred:       3340000 bytes
Requests per second:    1496.42 [#/sec] (mean)
Time per request:       6.683 [ms] (mean)
Time per request:       0.668 [ms] (mean, across all concurrent requests)
Transfer rate:          726.29 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.3      1       3
Processing:     3    6   1.1      6      22
Waiting:        3    5   1.0      5      22
Total:          3    7   1.1      6      24

Percentage of the requests served within a certain time (ms)
  50%      6
  66%      7
  75%      7
  80%      7
  90%      8
  95%      8
  98%      9
  99%     10
 100%     24 (longest request)
 ```
 
 # С балансировкой
 ```
puma@LAPTOP:/etc/nginx$ ab -l -c 10 -n 10000 http://localhost:2345/api/v1/gameboard
This is ApacheBench, Version 2.3 <$Revision: 1843412 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:        snake
Server Hostname:        localhost
Server Port:            2345

Document Path:          /api/v1/gameboard
Document Length:        Variable

Concurrency Level:      10
Time taken for tests:   3.753 seconds
Complete requests:      10000
Failed requests:        0
Non-2xx responses:      10000
Total transferred:      3110000 bytes
HTML transferred:       1660000 bytes
Requests per second:    2664.31 [#/sec] (mean)
Time per request:       3.753 [ms] (mean)
Time per request:       0.375 [ms] (mean, across all concurrent requests)
Transfer rate:          809.18 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   0.2      1       2
Processing:     1    3   0.4      3       6
Waiting:        1    2   0.3      2       5
Total:          2    4   0.4      4       7

Percentage of the requests served within a certain time (ms)
  50%      4
  66%      4
  75%      4
  80%      4
  90%      4
  95%      4
  98%      5
  99%      5
 100%      7 (longest request)
 ```
