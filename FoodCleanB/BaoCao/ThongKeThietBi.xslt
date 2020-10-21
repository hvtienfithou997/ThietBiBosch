<?xml version="1.0"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <html>
      <head>
        <meta charset="utf-8"/>
        <style type="text/css" rel="stylesheet">
          #wrap{
          width: 790px;
          margin: 10px auto;

          }
          table{
          border-spacing: 1;
          border-collapse: collapse;
          width: 100%;

          }
          table td,table th{
          padding: 5px;
          line-height: 1.32857143;
          vertical-align: top;
          border: 1px solid #ddd;
          }
          #container{
          width: 100%;
          }
          #logo{
          width: 100%;
          }
          .logo-left {
          width:20%;
          float: left;
          }
          .logo-right{
          width:80%;
          float: right;
          }
          .text-center{
          text-align: center;
          }
          #title{
          width:100%;
          margin-top:80px;
          }
          #wrap p{
          margin: 0;
          }
        </style>
      </head>
      <body>
        <div id="wrap">
          <xsl:for-each select="DKAC//DK">
            <div id="logo">
              <div  class="logo-left text-center">

                <p>TỔNG CÔNG TY</p>
                <P>CÔNG TY ABC</P>
                <P>LOGO</P>
              </div>
              <div class="logo-right text-center">
                <p>CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</p>
                <p>Độc lập - Tự do - Hạnh phúc</p>
              </div>
              <div style="clear: both;"></div>
            </div>
            <div id="container">
              <div id="title">
                <h4 class="text-center" style="font-weight: 600">THỐNG KÊ THIẾT BỊ BOSCH</h4>
                <p style="margin-left:200px;">
                  Họ tên: <xsl:value-of select="HoTen"/>
                </p>
                <p style="margin-left:200px;">
                  Phòng: <xsl:value-of select="PhongBan"/>
                </p>
              </div>
              <table style="margin-top:50px">
                <thead>
                  <tr>
                    <th>STT</th>
                    <th class="text-center">Ngày</th>
                    <th class="text-center">Ca 1</th>
                    <th class="text-center">Ca 2</th>
                    <th class="text-center">Ca 3</th>
                  </tr>
                </thead>
                <tbody>
                  <xsl:for-each select="Bang//Dong">
                    <tr>
                      <td>
                        <xsl:value-of select="position()"/>
                      </td>
                      <td class="text-center">
                        <xsl:value-of select="Ngay"/>
                      </td>
                      <td class="text-center">
                        <xsl:value-of select="Ca1"/>
                      </td>
                      <td class="text-center">
                        <xsl:value-of select="Ca2"/>
                      </td>
                      <td class="text-center">
                        <xsl:value-of select="Ca3"/>
                      </td>
                    </tr>
                  </xsl:for-each>
                  <tr style="font-weight: bold;">
                    <td>Tổng</td>
                    <td></td>
                    <td class="text-center">
                      <xsl:value-of select="TongCa1"/>

                    </td>
                    <td class="text-center">
                      <xsl:value-of select="TongCa2"/>
                    </td>
                    <td class="text-center">
                      <xsl:value-of select="TongCa3"/>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </xsl:for-each>
        </div>

      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
