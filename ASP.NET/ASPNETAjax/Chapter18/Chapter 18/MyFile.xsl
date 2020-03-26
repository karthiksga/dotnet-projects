<?xml version='1.0' encoding='utf-8' ?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <results>
            <xsl:apply-templates select="//Result" />
        </results>
    </xsl:template>
    <xsl:template match="//Result">
        <result title="{Title}" author="{Author}" amazonUrl="{AmazonUrl}"
        imageUrl="{ImageUrl}" listPrice="{ListPrice}" price="{Price}" />
    </xsl:template>
</xsl:stylesheet>