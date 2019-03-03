# -*- coding: utf-8 -*-
"""
/***************************************************************************
 MarketShare
                                 A QGIS plugin
 find market share
                             -------------------
        begin                : 2018-03-22
        copyright            : (C) 2018 by market
        email                : market@bigmarket.com
        git sha              : $Format:%H$
 ***************************************************************************/

/***************************************************************************
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 ***************************************************************************/
 This script initializes the plugin, making it known to QGIS.
"""


# noinspection PyPep8Naming
def classFactory(iface):  # pylint: disable=invalid-name
    """Load MarketShare class from file MarketShare.

    :param iface: A QGIS interface instance.
    :type iface: QgisInterface
    """
    #
    from .market_share import MarketShare
    return MarketShare(iface)
