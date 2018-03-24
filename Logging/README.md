# **Reports:**

## **to get full statistic on screen**
`LOGPARSER "SELECT Text FROM 'D:\.NET mentoring\Logging\MvcMusicStore\logs\2017-06-12.log'"  -i:TEXTLINE -q:Off`

## **to get report in file**
`LOGPARSER "SELECT Text INTO 'D:\.NET mentoring\Logging\MvcMusicStore\logs\report.txt' FROM 'D:\.NET mentoring\Logging\MvcMusicStore\logs\2017-06-12.log'"  -i:TEXTLINE -q:Off`

## **get number of INFO/ERROR/DEBUG**

`LOGPARSER "SELECT COUNT(Text) FROM 'D:\.NET mentoring\Logging\MvcMusicStore\logs\2017-06-12.log' WHERE Text LIKE '%INFO%'"  -i:TEXTLINE -q:Off`

`LOGPARSER "SELECT COUNT(Text) FROM 'D:\.NET mentoring\Logging\MvcMusicStore\logs\2017-06-12.log' WHERE Text LIKE '%DEBUG%'"  -i:TEXTLINE -q:Off`

`LOGPARSER "SELECT COUNT(Text) FROM 'D:\.NET mentoring\Logging\MvcMusicStore\logs\2017-06-12.log' WHERE Text LIKE '%ERROR%'"  -i:TEXTLINE -q:Off`

## **get info about error on screen**
`LOGPARSER "SELECT Text FROM 'D:\.NET mentoring\Logging\MvcMusicStore\logs\2017-06-12.log' WHERE Text LIKE '%ERROR%'"  -i:TEXTLINE -q:Off -e:10`
