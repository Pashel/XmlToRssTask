<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
                xmlns:cg="http://library.by/catalog"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform" 
                exclude-result-prefixes="cg">

  <xsl:output method="xml" indent="yes"/>

  <xsl:param name="Date" select="''"/>

  <xsl:template match="/">
    <xsl:element name="rss" namespace="http://library.by/catalog">
      <xsl:attribute name="version">2.0</xsl:attribute>
      <xsl:element name="chanel" namespace="http://library.by/catalog">
        <title>Library.by</title>
        <link>http://my.safaribooksonline.com</link>
        <description>Super puper library shop</description>
        <language>en-us</language>
        <pubDate>
          <xsl:value-of select="$Date"/>
        </pubDate>
        <xsl:apply-templates/>
      </xsl:element>
    </xsl:element>
  </xsl:template>

  <xsl:template match="cg:book">
    <xsl:element name="item" namespace="http://library.by/catalog">
      <!--<xsl:copy-of select="cg:title|cg:author|cg:description"/>-->
      <xsl:apply-templates select="cg:title|cg:author|cg:description|cg:publish_date|cg:isbn"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="cg:title|cg:author|cg:description">
    <xsl:element name="{name()}" namespace="http://library.by/catalog">
      <xsl:value-of select="text()"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="cg:publish_date">
    <xsl:element name="pubDate" namespace="http://library.by/catalog">
      <xsl:value-of select="text()"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="cg:isbn">
    <xsl:if test="text()">
      <xsl:element name="link" namespace="http://library.by/catalog">
        <xsl:value-of select="concat('http://my.safaribooksonline.com/', text())"/>
      </xsl:element>
    </xsl:if>
  </xsl:template>

  <xsl:template match="@*|text()"/>
  
  <!--<xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>-->

</xsl:stylesheet>