// Copyright (c) Microsoft. All rights reserved.

import React from 'react';
import { shallow } from 'enzyme';
import 'polyfills';

import { TenantManagementContainer } from './tenantManagement.container';

describe('Dashboard Component', () => {
  it('Renders without crashing', () => {

    const fakeProps = {
      rulesEntities: {},
      rulesError: undefined,
      rulesIsPending: false,
      rulesLastUpdated: undefined,
      deviceEntities: {},
      fetchRules: () => {},
      t: () => {},
      updateCurrentWindow: () => {},
      logEvent: () => {}
    };

    const wrapper = shallow(
      <MaintenanceContainer {...fakeProps} />
    );
  });
});